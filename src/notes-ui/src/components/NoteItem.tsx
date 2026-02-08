import type { Note } from '../types/note';

/**
 * Props for the NoteItem component
 */
export interface NoteItemProps {
  note: Note;
  onDelete: (id: string) => void;
}

/**
 * Formats a date string to a readable format
 * @param dateString - ISO date string
 * @returns Formatted date string
 */
function formatDate(dateString: string): string {
  const date = new Date(dateString);
  return date.toLocaleString(undefined, {
    year: 'numeric',
    month: 'short',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
  });
}

/**
 * Component to display an individual note
 */
export function NoteItem({ note, onDelete }: NoteItemProps) {
  const handleDelete = () => {
    onDelete(note.id);
  };

  return (
    <div className="note-item">
      <div className="note-item-header">
        <h3>{note.title}</h3>
        <button 
          onClick={handleDelete}
          className="delete-button"
          aria-label={`Delete note: ${note.title}`}
        >
          Delete
        </button>
      </div>
      <p className="note-content">{note.content}</p>
      <div className="note-dates">
        <span>Created: {formatDate(note.createdAt)}</span>
        <span>Updated: {formatDate(note.updatedAt)}</span>
      </div>
    </div>
  );
}
