using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace ProgramManager.SystemUtility
{
    /// <summary>
    /// Klasa do zarzadzania zmianami w <see cref="InstalledSoftware"/>.
    /// </summary>
    public class InstalledSoftwareHandler
    {
        private ObservableCollection<Software> _oldInstalledSoftWareList;
        private InstalledSoftware _installedSoftware;
        /// <summary>
        /// Przechowywanie zmienionych programow. Uzyty tryb wyliczeniowy <see cref="SoftwareChangeStatus"/> do przechowania rodzaju zmiany.
        /// </summary>
        public ObservableCollection<KeyValuePair<Software, SoftwareChangeStatus>> ChangedSoftwareDictionary { get; set; }

        /// <summary>
        /// Inicjalizacja instancji <see cref="InstalledSoftwareHandler"/>, gdzie pobierana jest instancja <see cref="InstalledSoftware"/>, 
        /// inicjalizowany jest kolekcja <see cref="_changedSoftwareDictionary"/>, 
        /// inicjalizowana jest kolekcja <see cref="_oldInstalledSoftWareList"/>
        /// na podstawie konstruktora <see cref="ObservableCollection{T}.ObservableCollection(List{T})"/>, ktory kopiuje elementy z <see cref="InstalledSoftware.InstalledSoftwareList"/>.
        /// Dodawana jest metoda <see cref="OnCollectionChanged"/> do eventu <see cref="ObservableCollection{T}.CollectionChanged"/>.
        /// </summary>
        public InstalledSoftwareHandler()
        {
            _installedSoftware = InstalledSoftware.GetInstance();
            ChangedSoftwareDictionary = new ObservableCollection<KeyValuePair<Software, SoftwareChangeStatus>>();
            _oldInstalledSoftWareList = new ObservableCollection<Software>(_installedSoftware.InstalledSoftwareList);
            _installedSoftware.InstalledSoftwareList.CollectionChanged += OnCollectionChanged;
        }

        /// <summary>
        /// Metoda dodana do eventu <see cref="ObservableCollection{T}.CollectionChanged"/>. 
        /// Wywolywana w momencie zmiany kolekcji <see cref="InstalledSoftware.InstalledSoftwareList"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

        /// <summary>
        /// Porownuje dwie kolekcje - <see cref="_oldInstalledSoftWareList"/> oraz <see cref="InstalledSoftware.InstalledSoftwareList"/>
        /// za pomoca <see cref="Enumerable.Except{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/>.
        /// Nastepnie dodaje wyniki do <see cref="_changedSoftwareDictionary"/>.
        /// </summary>
        private void CompareCollections()
        {
            var removed = _oldInstalledSoftWareList.Except(_installedSoftware.InstalledSoftwareList).ToList();
            var added = _installedSoftware.InstalledSoftwareList.Except(_oldInstalledSoftWareList).ToList();

            if (removed != null)
            {
                foreach (Software s in removed)
                    ChangedSoftwareDictionary.Add(new KeyValuePair<Software, SoftwareChangeStatus>(s, SoftwareChangeStatus.REMOVED));
            }
            if (added != null)
            {
                foreach (Software s in added)
                    ChangedSoftwareDictionary.Add(new KeyValuePair<Software, SoftwareChangeStatus>(s, SoftwareChangeStatus.ADDED));
            }
        }

        /// <summary>
        /// Metoda wywolywana w <see cref="UpdateTask.Tick(object, EventArgs)"/> celem aktualizacji listy programow.
        /// </summary>
        public void Update()
        {
            _oldInstalledSoftWareList = new ObservableCollection<Software>(_installedSoftware.InstalledSoftwareList);
            _installedSoftware.UpdateInstalledSoftwareList();
            CompareCollections();
            ChangedSoftwareDictionary.Add(new KeyValuePair<Software, SoftwareChangeStatus>(new Software("test", "1.0"), SoftwareChangeStatus.ADDED));
        }

    }
}
