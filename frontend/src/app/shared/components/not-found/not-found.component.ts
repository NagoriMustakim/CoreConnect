import { NzButtonModule } from 'ng-zorro-antd/button';
import { Router, ActivatedRoute } from '@angular/router';
import { Component } from '@angular/core';
import { NzResultModule } from 'ng-zorro-antd/result';
@Component({
  selector: 'app-not-found',
  standalone: true,
  imports: [NzResultModule, NzButtonModule],
  template: `
    <nz-result
      nzStatus="404"
      nzTitle="404"
      nzSubTitle="Sorry, the page you visited does not exist."
    >
      <div nz-result-extra>
        <button nz-button nzType="primary" (click)="backHome()">
          Back Home
        </button>
      </div>
    </nz-result>
  `,
})
export class NotFoundComponent {
  constructor(private router: Router,private route: ActivatedRoute) {}
  backHome() {
    this.router.navigate(['/'], { relativeTo: this.route });  }
}
