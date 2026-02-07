import type { Note, CreateNote, UpdateNote, ApiError } from '../types/note';

/**
 * Configuration for the API client
 * Uses Vite environment variable with fallback to default
 */
const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5138/api';

/**
 * Custom error class for API errors
 */
export class NotesApiError extends Error {
  statusCode: number;
  apiError?: ApiError;
  
  constructor(statusCode: number, apiError?: ApiError) {
    super(apiError?.message || 'An error occurred while communicating with the API');
    this.name = 'NotesApiError';
    this.statusCode = statusCode;
    this.apiError = apiError;
  }
}

/**
 * Helper function to handle API responses
 */
async function handleResponse<T>(response: Response): Promise<T> {
  if (!response.ok) {
    let apiError: ApiError | undefined;
    
    try {
      apiError = await response.json();
    } catch {
      // If parsing fails, continue without apiError
    }
    
    throw new NotesApiError(response.status, apiError);
  }
  
  // Handle 204 No Content responses
  if (response.status === 204) {
    return undefined as T;
  }
  
  return response.json();
}

/**
 * API client for notes operations
 */
export const notesApi = {
  /**
   * Retrieves all notes from the API
   * @returns Promise resolving to an array of notes
   */
  async getAllNotes(): Promise<Note[]> {
    const response = await fetch(`${API_BASE_URL}/notes`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    });
    
    return handleResponse<Note[]>(response);
  },

  /**
   * Retrieves a specific note by ID
   * @param id - The note ID
   * @returns Promise resolving to the note
   */
  async getNoteById(id: string): Promise<Note> {
    const response = await fetch(`${API_BASE_URL}/notes/${id}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    });
    
    return handleResponse<Note>(response);
  },

  /**
   * Creates a new note
   * @param note - The note data to create
   * @returns Promise resolving to the created note
   */
  async createNote(note: CreateNote): Promise<Note> {
    const response = await fetch(`${API_BASE_URL}/notes`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(note),
    });
    
    return handleResponse<Note>(response);
  },

  /**
   * Updates an existing note
   * @param id - The note ID to update
   * @param note - The updated note data
   * @returns Promise resolving to the updated note
   */
  async updateNote(id: string, note: UpdateNote): Promise<Note> {
    const response = await fetch(`${API_BASE_URL}/notes/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(note),
    });
    
    return handleResponse<Note>(response);
  },

  /**
   * Deletes a note
   * @param id - The note ID to delete
   * @returns Promise resolving when the note is deleted
   */
  async deleteNote(id: string): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/notes/${id}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
      },
    });
    
    return handleResponse<void>(response);
  },
};
