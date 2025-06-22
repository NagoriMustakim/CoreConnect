import { sharedConstant } from './../../../../shared/constants/sharedDocumentConstant';
import { environment } from './../../../../../environments/environment';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { CommonModule } from '@angular/common';
import { ResumeService } from './../../services/resume.service';
import { Component, OnInit } from '@angular/core';
import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-resume',
  standalone: true,
  imports: [CommonModule, NzButtonModule, NzGridModule],
  templateUrl: './resume.component.html',
  styleUrl: './resume.component.css',
})
export class ResumeComponent {
  resumeData!: any;
  logoImage!: any;
  businessUnitId: any;
  userId: any;
  experiences: any;

  handlePrint() {
    window.print();
  }
  constructor(private service: ResumeService, private route: ActivatedRoute) {
    this.route.queryParams.subscribe((params: any) => {
      this.userId = params.userId;
    });
    this.route.params.subscribe((params: any) => {
      this.businessUnitId = params.businessUnitId;
      console.log(this.businessUnitId);
    });

    this.service.getResumeData(this.businessUnitId, this.userId).subscribe({
      next: (res: any) => {
        this.resumeData = res;
        this.experiences = this.resumeData.experiences;
        console.log(this.resumeData);
      },
      error: (err) => {
        console.error(err.message);
      },
    });
  }

  async loadLogoAndWait(): Promise<void> {
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

  exportToPDF() {
    const data = document.getElementById(sharedConstant.CONTAINER);
    if (!data) {
      console.error(sharedConstant.ELEMENT_ID_NOT_FOUND);
      return;
    }

    html2canvas(data, { scale: 2 }).then((canvas) => {
      const imgWidth = 210;
      const pageHeight = 295;
      const imgHeight = 295;
      let heightLeft = imgHeight;
      const imgData = canvas.toDataURL(sharedConstant.IMAGE_PNG, 1.0);
      const pdf = new jsPDF(sharedConstant.P, sharedConstant.MM, sharedConstant.A4);
      let position = 0;

      pdf.addImage(
        imgData,
        sharedConstant.PNG,
        0,
        position,
        imgWidth,
        imgHeight,
        undefined,
        sharedConstant.NONE
      );
      pdf.save(this.resumeData.firstName + sharedConstant.RESUME_PDF);
    });
  }
}
