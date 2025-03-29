using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Entities
{
    public class Lesson : Entity<int>
    {

        private LessonName _lessonName;

        private string _description;

        private DateTime _date;

        private Domain.ValueObjects.File _material;

        private List<HomeTask> _homeTasks;

        public Lesson(int id,LessonName lessonName,
                      string description,
                      DateTime date,
                      Domain.ValueObjects.File material,
                      List<HomeTask> homeTasks) : base(id)
        {
            _lessonName = lessonName;
            _description = description;
            _date = date;
            _material = material;
            _homeTasks = homeTasks;
        }

        public LessonName GetName()
        {
            return _lessonName;
        }

        public List<HomeTask> GetHomeTasks()
        {
            return _homeTasks;
        }

        public (LessonName lessonName,
                string description,
                DateTime date,
                Domain.ValueObjects.File material,
                IReadOnlyList<HomeTask> homeTasks) GetLesson()
        {
            return (_lessonName,
                      _description,
                      _date,
                      _material,
                      _homeTasks.AsReadOnly());
        }
    }
}
