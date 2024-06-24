using ICS.BL.Models;

namespace ICS.App.ViewModels;

/// <summary>
/// Singleton class to hold shared data accessible across multiple view models.
/// </summary>
public sealed class SharedData
{
    /// <summary>
    /// Singleton instance of <see cref="SharedData"/>.
    /// This instance is lazily initialized to ensure it is created only when needed.
    /// </summary>
    private static readonly Lazy<SharedData> _instance = new(() => new SharedData());

    /// <summary>
    /// Private constructor to prevent instantiation from outside.
    /// This ensures that the class follows the singleton pattern.
    /// </summary>
    private SharedData() { }

    /// <summary>
    /// Gets the singleton instance of <see cref="SharedData"/>.
    /// This instance is used for accessing shared data across the application.
    /// </summary>
    public static SharedData Instance => _instance.Value;

    /// <inheritdoc cref="SelectedStudent"/>
    private StudentDetailModel _selectedStudent = StudentDetailModel.Empty;
    
    /// <summary>
    /// Gets or sets the currently selected student.
    /// This property is shared across multiple view models that inherit from <see cref="ViewModelBase"/>.
    /// Notifies when the selected student changes.
    /// </summary>
    public StudentDetailModel SelectedStudent
    {
        get => _selectedStudent;
        set
        {
            if (_selectedStudent != value)
            {
                _selectedStudent = value;
                OnSelectedStudentChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    
    /// <summary>
    /// Event triggered when the selected student changes.
    /// </summary>
    public event EventHandler? OnSelectedStudentChanged;
    
    /// <inheritdoc cref="SelectedSubject"/>
    private SubjectDetailModel _selectedSubject = SubjectDetailModel.Empty;
    
    /// <summary>
    /// Represents the currently selected subject.
    /// Its value is shared across multiple view models that inherit from <see cref="ViewModelBase"/>.
    /// </summary>
    public SubjectDetailModel SelectedSubject
    {
        get => _selectedSubject;
        set => _selectedSubject = value;
    }
}