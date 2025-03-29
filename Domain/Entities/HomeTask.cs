using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Entities
{
    public class HomeTask : Entity<int>
    {
        private HomeTaskName _homeTaskName;

        private string? _description;

        private Duration _duration;

        private Domain.ValueObjects.File _material;

        private List<HomeWork> _homeWorks;

        public HomeTask(int id,HomeTaskName homeTaskName,
                        string description,
                        Duration duration,
                        Domain.ValueObjects.File material) : base(id)
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
                
        private void UpdateMaterial(Domain.ValueObjects.File file)
        {
            _material = file;
        }
    }
}
