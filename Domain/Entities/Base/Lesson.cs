using Domain.Entities.Base;
using ValueObjects;

namespace Entities.Base
{
    public class Lesson : Entity<Lesson>
    {

        private LessonName _lessonName;

        private string _description;

        private DateTime _date;

        private ValueObjects.File _material;

        private List<HomeTask> _homeTasks;

        public Lesson(LessonName lessonName,
                      string description,
                      DateTime date,
                      ValueObjects.File material,
                      List<HomeTask> homeTasks)
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
                ValueObjects.File material,
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
