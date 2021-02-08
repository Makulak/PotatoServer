using AutoMapper;
using PotatoServer.ViewModels.Core;
using System.Collections.Generic;

namespace PotatoServer.Helpers.Extensions
{
    public static class MapperExtensions
    {
        public static PagedVmResult<TViewModel> MapPagedViewModel<TModel, TViewModel>(this IMapper mapper, PagedVmResult<TModel> pagedModel)
        {
            return new PagedVmResult<TViewModel>(mapper.Map<List<TModel>, List<TViewModel>>(pagedModel.Items), pagedModel.TotalItemsCount);
        }
    }
}
