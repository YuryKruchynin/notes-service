import type { Note } from '../types/note';
import { NoteItem } from './NoteItem';

/**
 * Props for the NotesList component
 */
export interface NotesListProps {
  notes: Note[];
  loading: boolean;
  error: string | null;
  onDelete: (id: string) => void;
}

/**
 * Component to display a list of notes with loading and error states
 */
export function NotesList({ notes, loading, error, onDelete }: NotesListProps) {
  // Show loading state
  if (loading) {
    return (
      <div className="notes-list-message">
        <p>Loading notes...</p>
      </div>
    );
  }

  // Show error state
  if (error) {
    return (
      <div className="notes-list-message error">
        <p>Error: {error}</p>
      </div>
    );
  }

  // Show empty state
  if (notes.length === 0) {
    return (
      <div className="notes-list-message empty">
        <p>No notes yet. Create your first note!</p>
      </div>
    );
  }

  // Show list of notes
  return (
    <div className="notes-list">
      {notes.map((note) => (
        <NoteItem key={note.id} note={note} onDelete={onDelete} />
      ))}
    </div>
  );
}
