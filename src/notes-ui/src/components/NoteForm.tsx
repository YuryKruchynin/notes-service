import { useState } from 'react';
import type { FormEvent } from 'react';
import type { CreateNoteDto } from '../types/note';

/**
 * Props for the NoteForm component
 */
export interface NoteFormProps {
  onSubmit: (data: CreateNoteDto) => void;
}

/**
 * Component for creating a new note
 */
export function NoteForm({ onSubmit }: NoteFormProps) {
  const [title, setTitle] = useState('');
  const [content, setContent] = useState('');
  const [errors, setErrors] = useState<{ title?: string; content?: string }>({});

  const validateForm = (): boolean => {
    const newErrors: { title?: string; content?: string } = {};

    if (!title.trim()) {
      newErrors.title = 'Title is required';
    }

    if (!content.trim()) {
      newErrors.content = 'Content is required';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (!validateForm()) {
      return;
    }

    const formData: CreateNoteDto = {
      title: title.trim(),
      content: content.trim(),
    };

    onSubmit(formData);

    // Clear form after successful submission
    setTitle('');
    setContent('');
    setErrors({});
  };

  return (
    <form onSubmit={handleSubmit} className="note-form">
      <div className="form-group">
        <label htmlFor="title">Title</label>
        <input
          id="title"
          type="text"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          placeholder="Enter note title"
          className={errors.title ? 'error' : ''}
        />
        {errors.title && <span className="error-message">{errors.title}</span>}
      </div>

      <div className="form-group">
        <label htmlFor="content">Content</label>
        <textarea
          id="content"
          value={content}
          onChange={(e) => setContent(e.target.value)}
          placeholder="Enter note content"
          rows={5}
          className={errors.content ? 'error' : ''}
        />
        {errors.content && <span className="error-message">{errors.content}</span>}
      </div>

      <button type="submit" className="submit-button">
        Create Note
      </button>
    </form>
  );
}
