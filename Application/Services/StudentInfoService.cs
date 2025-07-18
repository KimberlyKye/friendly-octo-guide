﻿using Application.Services.Abstractions;
using Application.Models.Teacher.Responses;
using RepositoriesAbstractions.Abstractions;
using Domain.ValueObjects.Enums;
using Entities;
using Application.Models.Course;
using Domain.ValueObjects;
using Application.Models.Lesson;

namespace Application.Services
{
    public class StudentInfoService : IStudentInfoService
    {
        private readonly IStudentInfoRepository _studentInfoRepository;

        public StudentInfoService(
            IStudentInfoRepository studentInfoRepository)
        {
            _studentInfoRepository = studentInfoRepository;
        }
        public async Task<List<StudentAllCoursesModel>> GetAllCourses(int studentId)
        {
            var courses = await _studentInfoRepository.GetAllCourses(studentId);
            
            var result = new List<StudentAllCoursesModel>();
            foreach (var course in courses)
            {
                result.Add(new StudentAllCoursesModel
                {
                    Id = course.Id,
                    Name = course.Name,
                    IsActive = course.State == CourseState.Active
                });
            }
            return result;
        }
        public async Task<CourseInfoForStudentModel?> GetCourseInfo(int courseId, int studentId)
        {

            var course = await _studentInfoRepository.GetCourseInfo(courseId, studentId);
            if (course is null) { return null; }

            return new CourseInfoForStudentModel
            {
                State = course.State,
                Teacher = course.Teacher,
                Name = course.Name,
                Description = course.Description,
                Duration = course.Duration
            };
        }
        public async Task<List<LessonInfoByCourseModel>?> GetLessonsInfoByCourse(int courseId, int studentId)
        {
            var course = await _studentInfoRepository.GetAllCourseInfo(courseId, studentId);
            if (course is null) { return null!; }

            return course.Lessons.Select(lesson => new LessonInfoByCourseModel
            {
                CourseId = course.Id,
                LessonName = lesson.Name,
                Description = lesson.Description,
                Date = lesson.Date,
                Material = lesson.Material,
                HomeTasks = lesson.HomeTasks.ToList()
            }).ToList();
        }
    }
}
