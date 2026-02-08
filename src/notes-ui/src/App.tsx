import './App.css';
import { useNotes } from './hooks/useNotes';
import { NotesList, NoteForm } from './components';

function App() {
  const { notes, loading, error, addNote, removeNote } = useNotes();

  const handleSubmit = async (data: { title: string; content: string }) => {
    try {
      await addNote(data);
    } catch (err) {
      console.error('Failed to add note:', err);
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await removeNote(id);
    } catch (err) {
      console.error('Failed to delete note:', err);
    }
  };

  return (
    <div className="app">
      <h1>Notes Service</h1>
      <div className="container">
        <div className="form-section">
          <h2>Create New Note</h2>
          <NoteForm onSubmit={handleSubmit} />
        </div>
        <div className="list-section">
          <h2>My Notes</h2>
          <NotesList 
            notes={notes} 
            loading={loading} 
            error={error} 
            onDelete={handleDelete} 
          />
        </div>
      </div>
    </div>
  );
}

export default App;
