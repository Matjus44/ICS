using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ICS.DAL.Entities;

namespace ICS.DAL.Mappers;

public class RatingEntityMapper : IEntityMapper<RatingEntity>
{
    public void MapToExistingEntity(RatingEntity existingEntity, RatingEntity updatedEntity)
    {
        existingEntity.Rating = updatedEntity.Rating;
        existingEntity.Note = updatedEntity.Note;
    }
}
