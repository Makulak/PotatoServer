using AutoMapper;
using PotatoServer.ViewModels.Core;
using System.Collections.Generic;

namespace PotatoServer.Helpers.Extensions
{
    public static class MapperExtensions
    {
        public static PagedViewModel<TViewModel> MapPagedViewModel<TModel, TViewModel>(this IMapper mapper, PagedViewModel<TModel> pagedModel)
        {
            return new PagedViewModel<TViewModel>(mapper.Map<List<TModel>, List<TViewModel>>(pagedModel.Items), pagedModel.TotalItemsCount)
        }
    }
}
