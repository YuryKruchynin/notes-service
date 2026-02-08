# Notes Service

A full-stack notes application with ASP.NET Core Web API backend and React frontend. This application allows users to create, list, update, and delete notes with a modern, responsive user interface.

## 🚀 Features

- **Create Notes**: Add new notes with title and content
- **List Notes**: View all your notes in an organized manner
- **Update Notes**: Edit existing notes
- **Delete Notes**: Remove notes you no longer need
- **Modern UI**: Built with React and TypeScript for a smooth user experience
- **RESTful API**: Clean and well-structured API built with ASP.NET Core

## 🛠️ Technology Stack

### Backend
- **ASP.NET Core Web API 10.0** - RESTful API framework
- **.NET 10** - Runtime and framework
- **In-Memory Storage** - For data persistence
- **Swagger/OpenAPI** - API documentation and testing

### Frontend
- **React 19** - UI library for building user interfaces
- **TypeScript 5.9** - Type-safe JavaScript
- **Vite 7** - Fast build tool and development server
- **ESLint** - Code quality and linting
- **CSS3** - Styling

## 📁 Project Structure

```
notes-service/
├── .github/
│   └── instructions/          # Project instructions and guidelines
├── Docs/                       # Documentation
│   └── project-structure.md   # Detailed project structure reference
├── src/                        # Source code
│   ├── Notes.Api/             # Backend API (.NET)
│   │   ├── Controllers/       # API endpoints
│   │   ├── DTOs/              # Data Transfer Objects
│   │   ├── Models/            # Domain models
│   │   ├── Repositories/      # Data access layer
│   │   ├── Services/          # Business logic
│   │   └── Program.cs         # Application entry point
│   └── notes-ui/              # Frontend UI (React + TypeScript + Vite)
│       ├── src/
│       │   ├── api/           # API client
│       │   ├── components/    # Reusable UI components
│       │   ├── hooks/         # Custom React hooks
│       │   ├── pages/         # Page components
│       │   ├── types/         # TypeScript type definitions
│       │   ├── App.tsx        # Main app component
│       │   └── main.tsx       # Application entry point
│       ├── package.json       # Dependencies and scripts
│       └── vite.config.ts     # Vite configuration
└── README.md                   # This file
```

For detailed project structure information, see [project-structure.md](./Docs/project-structure.md).

## ✅ Prerequisites

Before you begin, ensure you have the following installed:

### For Backend Development
- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 or later)
- A code editor (Visual Studio, Visual Studio Code, or JetBrains Rider recommended)

### For Frontend Development
- [Node.js](https://nodejs.org/) (version 18.x or later)
- [npm](https://www.npmjs.com/) package manager (comes with Node.js)
- A code editor (Visual Studio Code recommended)

### Optional
- [Git](https://git-scm.com/) for version control
- [Postman](https://www.postman.com/) or similar tool for API testing

## 🏗️ Setup Instructions

### Backend Setup

1. **Navigate to the backend directory**
   ```bash
   cd src/Notes.Api
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

The API will be available at:
- **HTTP**: `http://localhost:5138`
- **HTTPS**: `https://localhost:7257`

The API includes Swagger UI for testing and documentation, accessible at `http://localhost:5138/swagger` when the application is running.

### Frontend Setup

1. **Navigate to the frontend directory**
   ```bash
   cd src/notes-ui
   ```

2. **Install dependencies**
   ```bash
   npm install
   ```

3. **Configure environment variables (optional)**
   
   Copy the example environment file:
   ```bash
   cp .env.example .env
   ```
   
   The default configuration connects to `http://localhost:5138/api`. Update `.env` if your backend runs on a different port:
   ```env
   VITE_API_BASE_URL=http://localhost:5138/api
   ```

4. **Start the development server**
   ```bash
   npm run dev
   ```

The frontend application will be available at `http://localhost:5173` (default Vite port).

### Additional Frontend Commands

- **Build for production**
  ```bash
  npm run build
  ```

- **Preview production build**
  ```bash
  npm run preview
  ```

- **Lint code**
  ```bash
  npm run lint
  ```

## 🚀 Running the Application

To run the complete application, you need to start both the backend and frontend servers:

1. **Start the Backend** (in terminal 1)
   ```bash
   cd src/Notes.Api
   dotnet run
   ```
   Wait for the message indicating the server is running (usually shows `Now listening on: http://localhost:5138`)

2. **Start the Frontend** (in terminal 2)
   ```bash
   cd src/notes-ui
   npm run dev
   ```
   The frontend will open at `http://localhost:5173`

3. **Access the Application**
   - Frontend UI: `http://localhost:5173`
   - Backend API: `http://localhost:5138`
   - Swagger UI: `http://localhost:5138/swagger`

The frontend will automatically communicate with the backend API to perform CRUD operations on notes.

## 📚 API Documentation

The Notes Service API provides a RESTful interface for managing notes. All endpoints are prefixed with `/api/notes`.

### Base URL
- Development: `http://localhost:5138/api`
- Swagger UI: `http://localhost:5138/swagger`

### Endpoints

#### Get All Notes
```http
GET /api/notes
```

**Response:**
- **200 OK**: Returns an array of notes
- **500 Internal Server Error**: Server error occurred

**Example Response:**
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "title": "My First Note",
    "content": "This is the content of my note",
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-15T10:30:00Z"
  }
]
```

#### Get Note by ID
```http
GET /api/notes/{id}
```

**Parameters:**
- `id` (path parameter): GUID of the note

**Response:**
- **200 OK**: Returns the requested note
- **404 Not Found**: Note doesn't exist
- **500 Internal Server Error**: Server error occurred

#### Create Note
```http
POST /api/notes
```

**Request Body:**
```json
{
  "title": "My New Note",
  "content": "This is the content"
}
```

**Response:**
- **201 Created**: Note created successfully
- **400 Bad Request**: Invalid request data
- **500 Internal Server Error**: Server error occurred

**Example Response:**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "title": "My New Note",
  "content": "This is the content",
  "createdAt": "2024-01-15T10:30:00Z",
  "updatedAt": "2024-01-15T10:30:00Z"
}
```

#### Update Note
```http
PUT /api/notes/{id}
```

**Parameters:**
- `id` (path parameter): GUID of the note to update

**Request Body:**
```json
{
  "title": "Updated Title",
  "content": "Updated content"
}
```

**Response:**
- **200 OK**: Note updated successfully
- **404 Not Found**: Note doesn't exist
- **400 Bad Request**: Invalid request data
- **500 Internal Server Error**: Server error occurred

#### Delete Note
```http
DELETE /api/notes/{id}
```

**Parameters:**
- `id` (path parameter): GUID of the note to delete

**Response:**
- **204 No Content**: Note deleted successfully
- **404 Not Found**: Note doesn't exist
- **500 Internal Server Error**: Server error occurred

### Data Models

#### NoteDto
```typescript
{
  id: string;           // GUID
  title: string;        // Note title
  content: string;      // Note content
  createdAt: string;    // ISO 8601 datetime
  updatedAt: string;    // ISO 8601 datetime
}
```

#### CreateNoteDto
```typescript
{
  title: string;        // Note title (required)
  content: string;      // Note content (required)
}
```

#### UpdateNoteDto
```typescript
{
  title: string;        // Updated title (required)
  content: string;      // Updated content (required)
}
```

## 🔧 Troubleshooting

### Backend Issues

**Port already in use**
- Error: `Address already in use`
- Solution: Change the port in `src/Notes.Api/Properties/launchSettings.json` or stop the process using the port

**Build errors**
- Ensure you have .NET 8.0 or later installed: `dotnet --version`
- Try cleaning the build: `dotnet clean` then `dotnet build`

### Frontend Issues

**Port 5173 already in use**
- Solution: The dev server will automatically try the next available port (5174, 5175, etc.)
- Or manually specify a port in `vite.config.ts`

**API connection errors**
- Check that the backend is running on `http://localhost:5138`
- Verify the `VITE_API_BASE_URL` in your `.env` file
- Check browser console for CORS errors

**Node modules issues**
- Delete `node_modules` folder and `package-lock.json`
- Run `npm install` again

**ESLint errors**
- Run `npm run lint` to see all linting issues
- Most formatting issues can be auto-fixed

### Common Issues

**CORS errors when connecting frontend to backend**
- The backend is configured to allow requests from `http://localhost:5173`
- If using a different port, the backend CORS policy needs to be updated in `Program.cs`

**Changes not reflecting**
- Backend: Restart the `dotnet run` process
- Frontend: Vite has hot module replacement (HMR), but sometimes a browser refresh is needed

## 🤝 Contributing

Contributions are welcome! Please follow these guidelines:

1. **Fork the repository**
2. **Create a feature branch** (`git checkout -b feature/your-feature-name`)
3. **Commit your changes** (`git commit -m 'Add some feature'`)
4. **Push to the branch** (`git push origin feature/your-feature-name`)
5. **Open a Pull Request**

### Development Guidelines

- Follow the existing code style and conventions
- Write clear, descriptive commit messages
- Test your changes thoroughly before submitting
- Update documentation as needed
- Ensure your code passes all linting and build checks

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details (to be added).

## 📞 Support

For questions, issues, or suggestions, please open an issue in the GitHub repository.

---

**Built with ❤️ using ASP.NET Core and React**
