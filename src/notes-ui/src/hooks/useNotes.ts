import { useState, useEffect } from 'react';
import type { Note, CreateNoteDto } from '../types/note';
import { notesApi } from '../api/notesApi';

/**
 * Return type for the useNotes hook
 */
export interface UseNotesReturn {
  notes: Note[];
  loading: boolean;
  error: string | null;
  fetchNotes: () => Promise<void>;
  addNote: (data: CreateNoteDto) => Promise<void>;
  removeNote: (id: string) => Promise<void>;
}

/**
 * Custom hook for managing notes state and CRUD operations
 * 
 * @returns Object containing notes state and methods for CRUD operations
 * 
 * @example
 * ```tsx
 * const { notes, loading, error, fetchNotes, addNote, removeNote } = useNotes();
 * 
 * // Add a new note
 * await addNote({ title: 'New Note', content: 'Note content' });
 * 
 * // Remove a note
 * await removeNote('note-id');
 * ```
 */
export function useNotes(): UseNotesReturn {
  const [notes, setNotes] = useState<Note[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  /**
   * Fetches all notes from the API
   */
  const fetchNotes = async (): Promise<void> => {
    setLoading(true);
    setError(null);
    
    try {
      const fetchedNotes = await notesApi.getNotes();
      setNotes(fetchedNotes);
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Failed to fetch notes';
      setError(errorMessage);
    } finally {
      setLoading(false);
    }
  };

  /**
   * Adds a new note to the list
   * @param data - The note data to create
   */
  const addNote = async (data: CreateNoteDto): Promise<void> => {
    setError(null);
    
    try {
      const newNote = await notesApi.createNote(data);
      setNotes((prevNotes) => [...prevNotes, newNote]);
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Failed to add note';
      setError(errorMessage);
      throw err; // Re-throw to allow caller to handle
    }
  };

  /**
   * Removes a note from the list
   * @param id - The ID of the note to remove
   */
  const removeNote = async (id: string): Promise<void> => {
    setError(null);
    
    try {
      await notesApi.deleteNote(id);
      setNotes((prevNotes) => prevNotes.filter((note) => note.id !== id));
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Failed to remove note';
      setError(errorMessage);
      throw err; // Re-throw to allow caller to handle
    }
  };

  // Fetch notes on mount
  useEffect(() => {
    fetchNotes();
  }, []);

  return {
    notes,
    loading,
    error,
    fetchNotes,
    addNote,
    removeNote,
  };
}
