using MISA.Web11.Core.Entities;
using MISA.Web11.Core.Interfaces.Infrastructure;
using MISA.Web11.Core.Interfaces.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Service
{
    public class TeacherService : BaseService<Teacher>,ITeacherService
    {
        ITeacherRepository _teacherRepository;
        ISubjectRepository _subjectRepository;
        IDepartmentRepository _departmentRepository;
        ISubjectGroupRepository _subjectGroupRepository;

        public TeacherService(ITeacherRepository _teacherRepository, ISubjectRepository _subjectRepository, IDepartmentRepository _departmentRepository, ISubjectGroupRepository _subjectGroupRepository) :base(_teacherRepository)
        {
            this._teacherRepository = _teacherRepository;
            this._subjectRepository = _subjectRepository;
            this._departmentRepository = _departmentRepository;
            this._subjectGroupRepository = _subjectGroupRepository;
        }

        public IEnumerable<object> GetTeacherFull(int PageSize, int PageNumber, string TextSearch)
        {
            var teachers = _teacherRepository.Search(PageSize, PageNumber, TextSearch);
            var props = typeof(Teacher).GetProperties();
            var teacherCustoms = new List<object>();
            foreach (var teacher in teachers)
            {
                var subjects = _subjectRepository.GetSubjectByTearchId(teacher.TeacherId);
                var departments = _departmentRepository.GetDepartmentByTearchId(teacher.TeacherId);
                var subjectGroupId = teacher.SubjectGroupId;


                var teacherCustom = new SortedList();
                foreach (var prop in props)
                {
                    // Lấy tên của property:
                    var propName = prop.Name;
                    // Lấy value của property
                    var propValue = prop.GetValue(teacher);
                    teacherCustom.Add($"{propName}", propValue);
                }
                if (subjectGroupId != null)
                {
                    var subjectGroup = _subjectGroupRepository.Get(subjectGroupId);
                    teacherCustom.Add("SubjectGroup", subjectGroup);

                }
                var subjectIdList = new List<Guid>();
                var subjectNameList = new List<String>();
                //Merge teacher and department
                foreach (var subject in subjects)
                {
                    subjectIdList.Add(subject.SubjectId);
                    subjectNameList.Add(subject.SubjectName);
                }
                teacherCustom.Add("SubjectNames", subjectNameList);
                teacherCustom.Add("SubjectIds", subjectIdList);

                //Merge teacher and department
                var departmentIdList = new List<Guid>();
                var departmentNameList = new List<String>();
                foreach (var department in departments)
                {
                    departmentIdList.Add(department.DepartmentId);
                    departmentNameList.Add(department.DepartmentName);
                }
                teacherCustom.Add("DepartmentIds", departmentIdList);
                teacherCustom.Add("DepartmentNames", departmentNameList);

                teacherCustoms.Add(teacherCustom);
            }
            return teacherCustoms;
        }
    
        public int InsertTeacherServive(TeacherCustom teacherCustom)
        {
            return _teacherRepository.InsertTeacherRepository(teacherCustom);
        }
    }
}
