/**
 * Represents a note entity returned from the API
 */
export interface Note {
  id: string;
  title: string;
  content: string;
  createdAt: string;
  updatedAt: string;
}

/**
 * Data transfer object for creating a new note
 */
export interface CreateNoteDto {
  title: string;
  content: string;
}

/**
 * Data transfer object for updating an existing note
 */
export interface UpdateNote {
  title: string;
  content: string;
}

/**
 * API error response structure
 */
export interface ApiError {
  message: string;
}
