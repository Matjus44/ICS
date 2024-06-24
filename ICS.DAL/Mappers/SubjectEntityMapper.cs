using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ICS.DAL.Entities;

namespace ICS.DAL.Mappers;

public class SubjectEntityMapper : IEntityMapper<SubjectEntity>
{
    public void MapToExistingEntity(SubjectEntity existingEntity, SubjectEntity updatedEntity)
    {
        existingEntity.Name = updatedEntity.Name;
        existingEntity.CodeName = updatedEntity.CodeName;
    }
}
