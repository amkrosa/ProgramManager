using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramManager.SystemUtility
{
    /// <summary>
    /// Model jednego programu wraz z podstawowymi informacjami. Implementuje <see cref="IEquatable{T}"/>
    /// by moc porownywac listy zlozone z tej klasy.
    /// </summary>
    class Software : IEquatable<Software>
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public bool isUpdateNeeded { get; set; }
        /// <summary>
        /// Inicjalizuje nowa instancje klasy <see cref="Software"/>. Wartosc <see cref="isUpdateNeeded"/> ustawiana jest na <c>false</c>.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="version"></param>
        public Software(string name, string version)
        {
            this.Name = name;
            this.Version = version;
            isUpdateNeeded = false;
        }
        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Software s = (Software)obj;
            bool isNameEqual = this.Name == s.Name;
            bool isVersionEqual = this.Version == s.Version;
            return isNameEqual && isVersionEqual;
        }

        bool IEquatable<Software>.Equals(Software other) => Equals(other);
        public override int GetHashCode() => (Name, Version).GetHashCode();

    }
}
