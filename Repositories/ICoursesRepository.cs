using Backendv2.Models.Courses;

namespace Backendv2.Repositories
{
    public interface ICoursesRepository
    {
        void CreateCourse(string name, string description);
        void UpdateCourse(int id, string name, string description);
        IList<CourseModel> GetCourses();
        CourseDetailsModel GetById(int id);
        void DeleteCourse(int Id);

    }
}
