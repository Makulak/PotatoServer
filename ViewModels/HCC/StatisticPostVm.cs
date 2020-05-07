using PotatoServer.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace PotatoServer.ViewModels.HCC
{
    public class StatisticPostVm
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public DateTime? StartDateTime { get; set; }
        [Required]
        public DateTime? EndDateTime { get; set; }
        [Required]
        [Minimum(0)]
        public int? NumberOfMistakes { get; set; }
        [Required]
        public string ControlName { get; set; }
    }
}
