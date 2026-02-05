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
- **ASP.NET Core Web API** - RESTful API framework
- **.NET** - Runtime and framework

### Frontend
- **React** - UI library
- **TypeScript** - Type-safe JavaScript
- **Vite** - Fast build tool and dev server

## 📁 Project Structure

```
notes-service/
├── .github/
│   └── instructions/          # Project instructions and guidelines
├── Docs/                       # Documentation
│   └── project-structure.md   # Detailed project structure reference
├── src/                        # Source code
│   ├── Notes.Api/             # Backend API (.NET)
│   └── notes-ui/              # Frontend UI (React + TypeScript + Vite)
└── README.md                   # This file
```

For detailed project structure information, see [project-structure.md](./Docs/project-structure.md).

## ✅ Prerequisites

Before you begin, ensure you have the following installed:

### For Backend Development
- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or later)
- A code editor (Visual Studio, Visual Studio Code, or JetBrains Rider recommended)

### For Frontend Development
- [Node.js](https://nodejs.org/) (version 16.x or later)
- [npm](https://www.npmjs.com/) or [yarn](https://yarnpkg.com/) package manager
- A code editor (Visual Studio Code recommended)

### Optional
- [Git](https://git-scm.com/) for version control
- [Postman](https://www.postman.com/) or similar tool for API testing

## 🏗️ Setup Instructions

### Backend Setup

> **Note**: Detailed backend setup instructions will be added as the API is developed.

```bash
# Navigate to the backend directory
cd src/Notes.Api

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run
```

The API will be available at `https://localhost:5001` (or the port specified in your configuration).

### Frontend Setup

> **Note**: Detailed frontend setup instructions will be added as the UI is developed.

```bash
# Navigate to the frontend directory
cd src/notes-ui

# Install dependencies
npm install

# Start the development server
npm run dev
```

The application will be available at `http://localhost:5173` (default Vite port).

## 📚 API Documentation

> **Note**: Comprehensive API documentation will be added here once the API endpoints are implemented.

### Planned Endpoints

The Notes Service API will provide the following endpoints:

- `GET /api/notes` - Retrieve all notes
- `GET /api/notes/{id}` - Retrieve a specific note by ID
- `POST /api/notes` - Create a new note
- `PUT /api/notes/{id}` - Update an existing note
- `DELETE /api/notes/{id}` - Delete a note

Documentation format will include:
- Request/response examples
- Parameter descriptions
- Status codes
- Error handling

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
