using ICS.BL.Facades.Interfaces;
using ICS.BL.Mappers;
using ICS.BL.Mappers.Interfaces;
using ICS.BL.Models;
using ICS.DAL.Entities;
using ICS.DAL.Mappers;
using ICS.DAL.UnitOfWork;

namespace ICS.BL.Facades;

public class StudentsSubjectsFacade(
    IUnitOfWorkFactory unitOfWorkFactory,
    IStudentsSubjectsModelMapper modelMapper)
    :
        FacadeBase<StudentsSubjectsEntity, StudentsSubjectsListModel, StudentsSubjectsDetailModel, StudentsSubjectsEntityMapper>(
            unitOfWorkFactory, modelMapper), IStudentSubjectsFacade;