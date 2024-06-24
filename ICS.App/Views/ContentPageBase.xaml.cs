using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICS.App.ViewModels;
using ICS.BL.Models;

namespace ICS.App.Views;

public partial class ContentPageBase
{
    protected IViewModel ViewDetailModel { get; }
    
    public ContentPageBase(IViewModel viewDetailModel)
    {
        InitializeComponent();
        BindingContext = ViewDetailModel = viewDetailModel;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        await ViewDetailModel.OnAppearingAsync();
    }
    
    protected int GetRow(ActivityDetailModel activity)
    {
        return (int)activity.Start.DayOfWeek;
    }
    
    protected int GetStartColumn(ActivityDetailModel activity)
    {
        return activity.Start.Hour - 7;
    }
    
    protected int GetEndColumn(ActivityDetailModel activity)
    {
        return activity.Finish.Hour - 7;
    }
}