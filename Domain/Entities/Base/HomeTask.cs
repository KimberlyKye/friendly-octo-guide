using Domain.Entities.Base;
using ValueObjects;

namespace Entities.Base
{
    public class HomeTask : Entity<HomeTask>
    {

        private HomeTaskName _homeTaskName;

        private string? _description;

        private Duration _duration;

        private ValueObjects.File _material;

        private List<HomeWork> _homeWorks;

        public HomeTask(HomeTaskName homeTaskName,
                        string description,
                        Duration duration,
                        ValueObjects.File material)
        {
            _homeTaskName = homeTaskName;
            _description = description;
            _duration = duration;
            _material = material;
        }

        public HomeTaskName GetName()
        {
            return _homeTaskName;
        }

        public List<HomeWork> GetHomeWork()
        {
            return _homeWorks;
        }

        public (HomeTaskName homeTaskName,
                string description,
                Duration duration,
                ValueObjects.File material,
                IReadOnlyList<HomeWork> homeWork) GetHomeTask()
        {
            return (_homeTaskName,
                    _description,
                    _duration,
                    _material,
                    _homeWorks.AsReadOnly());
        }

        private void UpdateMaterial(ValueObjects.File file)
        {
            _material = file;
        }
    }
}
