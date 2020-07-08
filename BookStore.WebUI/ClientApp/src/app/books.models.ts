export interface Book {
  id: number,
  name: string,
  isbn: string,
  authorId: number,
  author: {
    id: number,
    fullName: string
  }
};

export interface BookList {
  books: Book[]
};

export interface Author {
  fullName: string,
  id: number
};

export interface AuthorList {
  authors: Author[]
}
