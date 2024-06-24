using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ICS.DAL.Entities;

namespace ICS.DAL.Mappers;

public class ActivityEntityMapper : IEntityMapper<ActivityEntity>
{
    public void MapToExistingEntity(ActivityEntity existingEntity, ActivityEntity updatedEntity)
    {
        existingEntity.Start = updatedEntity.Start;
        existingEntity.Finish = updatedEntity.Finish;
        existingEntity.Room = updatedEntity.Room;
        existingEntity.Type = updatedEntity.Type;
        existingEntity.ActivityDescription = updatedEntity.ActivityDescription;
    }
}
