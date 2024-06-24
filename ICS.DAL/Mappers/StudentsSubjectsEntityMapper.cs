using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ICS.DAL.Entities;

namespace ICS.DAL.Mappers;

public class StudentsSubjectsEntityMapper : IEntityMapper<StudentsSubjectsEntity>
{
    public void MapToExistingEntity(StudentsSubjectsEntity existingEntity, StudentsSubjectsEntity updatedEntity)
    {
        existingEntity.StudentId = updatedEntity.StudentId;
        existingEntity.SubjectId = updatedEntity.SubjectId;
        

        // tieto 2 mozno odstranit, lebo sa asi nemenia??
        existingEntity.Student = updatedEntity.Student;
        existingEntity.Subject = updatedEntity.Subject;
    }
}
