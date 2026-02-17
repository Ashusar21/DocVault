import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-upload',
  standalone: true,
  imports: [CommonModule, HttpClientModule, FormsModule],
  templateUrl: './upload.html',
  styleUrls: ['./upload.css'],
})
export class UploadComponent {
  selectedFile: File | null = null;
  message = '';
  searchTerm = '';
  documents: any[] = [];

  private apiUrl = 'https://localhost:5001/api/documents';

  constructor(private http: HttpClient) {}

  // =========================
  // Upload
  // =========================
  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
    }
  }

  upload() {
    if (!this.selectedFile) {
      this.message = 'Please select a file first';
      return;
    }

    const formData = new FormData();
    formData.append('file', this.selectedFile);

    this.http.post(`${this.apiUrl}/upload`, formData).subscribe({
      next: () => {
        this.message = 'Upload successful ✅';
        this.loadDocuments();
      },
      error: () => (this.message = 'Upload failed ❌'),
    });
  }

  // =========================
  // Search
  // =========================
  loadDocuments() {
    this.http.get<any[]>(`${this.apiUrl}?search=${this.searchTerm}`).subscribe({
      next: (data) => (this.documents = data),
      error: () => console.error('Error loading documents'),
    });
  }

  search() {
    this.loadDocuments();
  }

  // =========================
  // Download
  // =========================
  download(id: string) {
    window.open(`${this.apiUrl}/${id}/download`, '_blank');
  }

  // Auto load on start
  ngOnInit() {
    this.loadDocuments();
  }
}
