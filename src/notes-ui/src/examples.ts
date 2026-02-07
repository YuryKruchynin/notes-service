/**
 * Example usage of the Notes API client
 * 
 * This file demonstrates how to use the notesApi client to interact with the backend API.
 * It is not part of the production build but serves as documentation and examples.
 */

import { notesApi, NotesApiError } from './api';
import type { CreateNoteDto, UpdateNote } from './types';

/**
 * Example: Fetch all notes
 */
async function getAllNotesExample() {
  try {
    const notes = await notesApi.getNotes();
    console.log('All notes:', notes);
    return notes;
  } catch (error) {
    if (error instanceof NotesApiError) {
      console.error(`API Error ${error.statusCode}:`, error.message);
    } else {
      console.error('Unexpected error:', error);
    }
  }
}

/**
 * Example: Get a specific note by ID
 */
async function getNoteByIdExample(id: string) {
  try {
    const note = await notesApi.getNoteById(id);
    console.log('Note:', note);
    return note;
  } catch (error) {
    if (error instanceof NotesApiError) {
      if (error.statusCode === 404) {
        console.error('Note not found');
      } else {
        console.error(`API Error ${error.statusCode}:`, error.message);
      }
    } else {
      console.error('Unexpected error:', error);
    }
  }
}

/**
 * Example: Create a new note
 */
async function createNoteExample() {
  const newNote: CreateNoteDto = {
    title: 'My New Note',
    content: 'This is the content of my new note.',
  };

  try {
    const createdNote = await notesApi.createNote(newNote);
    console.log('Created note:', createdNote);
    return createdNote;
  } catch (error) {
    if (error instanceof NotesApiError) {
      console.error(`API Error ${error.statusCode}:`, error.message);
    } else {
      console.error('Unexpected error:', error);
    }
  }
}

/**
 * Example: Update an existing note
 */
async function updateNoteExample(id: string) {
  const updatedNote: UpdateNote = {
    title: 'Updated Note Title',
    content: 'This is the updated content.',
  };

  try {
    const note = await notesApi.updateNote(id, updatedNote);
    console.log('Updated note:', note);
    return note;
  } catch (error) {
    if (error instanceof NotesApiError) {
      if (error.statusCode === 404) {
        console.error('Note not found');
      } else {
        console.error(`API Error ${error.statusCode}:`, error.message);
      }
    } else {
      console.error('Unexpected error:', error);
    }
  }
}

/**
 * Example: Delete a note
 */
async function deleteNoteExample(id: string) {
  try {
    await notesApi.deleteNote(id);
    console.log('Note deleted successfully');
  } catch (error) {
    if (error instanceof NotesApiError) {
      if (error.statusCode === 404) {
        console.error('Note not found');
      } else {
        console.error(`API Error ${error.statusCode}:`, error.message);
      }
    } else {
      console.error('Unexpected error:', error);
    }
  }
}

/**
 * Example: Complete CRUD workflow
 */
async function completeCRUDExample() {
  // 1. Get all notes
  console.log('1. Fetching all notes...');
  await getAllNotesExample();

  // 2. Create a new note
  console.log('\n2. Creating a new note...');
  const createdNote = await createNoteExample();

  if (createdNote) {
    // 3. Get the specific note
    console.log('\n3. Fetching the created note...');
    await getNoteByIdExample(createdNote.id);

    // 4. Update the note
    console.log('\n4. Updating the note...');
    await updateNoteExample(createdNote.id);

    // 5. Delete the note
    console.log('\n5. Deleting the note...');
    await deleteNoteExample(createdNote.id);
  }
}

// Export examples for use in components or testing
export {
  getAllNotesExample,
  getNoteByIdExample,
  createNoteExample,
  updateNoteExample,
  deleteNoteExample,
  completeCRUDExample,
};
