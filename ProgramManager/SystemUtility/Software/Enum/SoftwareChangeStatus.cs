using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramManager.SystemUtility
{
    /// <summary>
    /// 
    /// </summary>
    public enum SoftwareChangeStatus
    {
        /// <summary>
        /// Program dodany do listy programow
        /// </summary>
        ADDED, 
        /// <summary>
        /// Program usuniety z listy programow
        /// </summary>
        REMOVED, 
        /// <summary>
        /// Program zaktualizowany w liscie programow
        /// </summary>
        UPDATED
    }
}
