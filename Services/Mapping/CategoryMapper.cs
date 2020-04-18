using PotatoServer.Database.Models.Camasutra;
using PotatoServer.ViewModels.Camasutra.Category;
using System.Collections.Generic;
using System.Linq;

namespace PotatoServer.Services.Mapping
{
    public class CategoryMapper
    {
        private readonly PositionMapper _positionMapper;

        public CategoryMapper(PositionMapper positionMapper)
        {
            _positionMapper = positionMapper;
        }

        public Category MapToCategory(CategoryPostVm categoryVm)
        {
            if (categoryVm == null)
                return null;

            return new Category
            {
                Name = categoryVm.Name
            };
        }

        public Category MapToCategory(CategoryPutVm categoryVm)
        {
            if (categoryVm == null)
                return null;

            return new Category
            {
                Id = categoryVm.Id,
                Name = categoryVm.Name
            };
        }

        public CategoryGetVm MapToCategoryGetVm(Category category)
        {
            if (category == null)
                return null;

            return new CategoryGetVm
            {
                Id = category.Id,
                Name = category.Name,
                Positions = _positionMapper.MapToPositionGetVm(category.Positions?.ToList())
            };
        }

        public IEnumerable<CategoryGetVm> MapToCategoryGetVm(IEnumerable<Category> categories)
        {
            if (categories == null)
                return null;

            return categories.Select(category => MapToCategoryGetVm(category));
        }
    }
}
