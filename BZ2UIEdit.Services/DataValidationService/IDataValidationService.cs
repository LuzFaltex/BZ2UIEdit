using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BZ2UIEdit.Services.DataValidationService
{
    /// <summary>
    /// This interface represents a service which helps with form data validation for the New Project form.
    /// </summary>
    public interface IDataValidationService<TNotifyDataErrorInfo>
        where TNotifyDataErrorInfo : INotifyDataErrorInfo
    {
        
    }
}
