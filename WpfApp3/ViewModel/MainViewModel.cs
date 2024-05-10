using NoteApp.Models;
using NoteApp.Utilities;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WpfApp3.ViewModel;
using WpfApp3.ViewModel.Helpers;

namespace WpfApp3.ViewModels
{
    internal class MainViewModel : BindingHelper
    {
        private const string FilePath = "notes.json";
        private ObservableCollection<Note> notes = new ObservableCollection<Note>();
        private ObservableCollection<Note> displayedNotes = new ObservableCollection<Note>(); // Для отображения данных в ListBox
        private Note selectedNote = new Note();
        private RelayCommand addCommand;
        private RelayCommand saveCommand;
        private RelayCommand deleteCommand;

        public MainViewModel()
        {
            AddCommand = new RelayCommand(AddNote);
            SaveCommand = new RelayCommand(SaveNote, () => SelectedNote != null);
            DeleteCommand = new RelayCommand(DeleteNote, () => SelectedNote != null);
            DisplayedNotes = new ObservableCollection<Note>(Notes); // Инициализация данных для отображения
        }

        public ObservableCollection<Note> Notes
        {
            get { return notes; }
            set { SetProperty(ref notes, value); }
        }

        public ObservableCollection<Note> DisplayedNotes
        {
            get { return displayedNotes; }
            set { SetProperty(ref displayedNotes, value); }
        }

        public Note SelectedNote
        {
            get { return selectedNote; }
            set
            {
                SetProperty(ref selectedNote, value);
                SaveCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand AddCommand
        {
            get { return addCommand; }
            set { SetProperty(ref addCommand, value); }
        }

        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
            set { SetProperty(ref saveCommand, value); }
        }

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
            set { SetProperty(ref deleteCommand, value); }
        }

        private void AddNote()
        {
            // Создание новой заметки с текущими значениями из текстовых полей
            Note newNote = new Note
            {
                Title = SelectedNote.Title,
                Description = SelectedNote.Description
            };
            Notes.Add(newNote);
            DisplayedNotes.Add(newNote); // Добавление новой заметки для отображения
            JsonSerializer.Serialize(Notes.ToList(), FilePath);
        }


        private void SaveNote()
        {
            JsonSerializer.Serialize(Notes.ToList(), FilePath);
        }

        private void DeleteNote()
        {
            Notes.Remove(SelectedNote);
            DisplayedNotes.Remove(SelectedNote); // Удаление заметки из отображаемых данных
            JsonSerializer.Serialize(Notes.ToList(), FilePath);
            SelectedNote = null;
        }
    }
}
