﻿using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;

namespace PotatoServer.Helpers
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class MinimumAttribute : ValidationAttribute
    {
        private IStringLocalizer<SharedResources> _localizer;
        public int MinimumValue { get; private set; }

        public MinimumAttribute(int minValue)
        {
            MinimumValue = minValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            _localizer = validationContext.GetService(typeof(IStringLocalizer<SharedResources>)) as IStringLocalizer<SharedResources>;

            return base.IsValid(value, validationContext);
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            if (value is int)
                return HandleInt((int)value);

            if (_localizer != null)
                throw new InvalidOperationException(_localizer.GetString("Annotation_NotSupportedObjectType", value.GetType().Name));
                
            throw new InvalidOperationException($"Object type {value.GetType().Name} is not supported");
        }

        public override string FormatErrorMessage(string name)
        {
            if (_localizer != null)
                return _localizer.GetString("Annotation_ValueMustBeMinimum", name, MinimumValue);

            return $"Value for {name} must at least {MinimumValue}.";
        }

        private bool HandleInt(int intValue)
        {
            if (intValue >= MinimumValue)
                return true;
            else
                return false;
        }
    }
}
