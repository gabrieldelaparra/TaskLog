using System;
using System.Collections.Generic;
using System.Text;

namespace TaskLog.Core.Services
{
    public interface IFileConfiguration
    {
        string Filename { get; set; }
    }
}
