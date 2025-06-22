import { environment } from './../../../../../environments/environment';
import { sharedConstant } from './../../../../shared/constants/sharedDocumentConstant';
import { CommonModule } from '@angular/common';
import { ResumeService } from './../../services/resume.service';
import { Component, OnInit } from '@angular/core';
import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-resume',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './resume.component.html',
  styleUrl: './resume.component.css',
})
export class ResumeComponent implements OnInit {
  resumeData!: any;
  logoImage!: any;
  businessUnitId: any;
  userId: any;

  handlePrint() {
    window.print();
  }
  constructor(private service: ResumeService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe((params: any) => {
      this.userId = params.userId;

      this.route.params.subscribe((params: any) => {
        this.businessUnitId = params.businessUnitId;
      });

      this.service.getResumeData(this.businessUnitId, this.userId).subscribe({
        next: (res: any) => {
          this.resumeData = res;
          console.log(res);
        },
        error: (err) => {
          console.error(err.message);
        },
      });
    });
  }

  preloadImage(url: string): Promise<HTMLImageElement> {
    return new Promise((resolve, reject) => {
      const img = new Image();
      img.onload = () => resolve(img);
      img.onerror = reject;
      img.src = url;
    });
  }

  async loadLogo(): Promise<void> {
    const logoUrl = `${environment.imageUrl}/${this.resumeData.businessUnit.businessUnitLogoName}`;
    const logoKey = sharedConstant.LOGO_IMAGE;
    this.logoImage = localStorage.getItem(logoKey);
    if (!this.logoImage) {
      try {
        const response = await fetch(logoUrl);
        const blob = await response.blob();
        const reader = new FileReader();
        reader.readAsDataURL(blob);
        reader.onloadend = () => {
          localStorage.setItem(logoKey, reader.result as string);
          this.logoImage = reader.result as string;
        };
      } catch (error) {
        console.error(error);
      }
    }

  }

  async exportToPDF() {
    const data = document.getElementById('container');
    if (!data) {
      console.error(sharedConstant.ELEMENT_ID_NOT_FOUND);
      return;
    }

    html2canvas(data).then((canvas) => {
      const imgWidth = 210;
      const pageHeight = 295;
      // const imgHeight = (canvas.height * imgWidth) / canvas.width;
      const imgHeight = 295
      let heightLeft = imgHeight;
      const imgData = canvas.toDataURL(sharedConstant.IMAGE_PNG);
      const pdf = new jsPDF('p', 'mm', 'a4');
      let position = 0;

      pdf.addImage(imgData, sharedConstant.PNG, 0, position, imgWidth, imgHeight);
      pdf.save(this.resumeData.firstName + sharedConstant.RESUME_PDF);
    });
  }
}

