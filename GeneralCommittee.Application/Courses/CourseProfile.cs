using AutoMapper;
using GeneralCommittee.Application.Courses.Commands.Create;
using GeneralCommittee.Domain.Dtos;
using GeneralCommittee.Domain.Entities;

namespace GeneralCommittee.Application.Courses
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CreateCourseCommand, Course>().ReverseMap();
            CreateMap<CourseMateriel, CourseMaterielDto>().ReverseMap();
            CreateMap<Course, CourseViewDto>().ReverseMap();
            CreateMap<InstructorDto, Instructor>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
        }
    }
}
