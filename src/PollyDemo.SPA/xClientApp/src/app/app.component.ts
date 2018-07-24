import { Component } from '@angular/core';
import { CompanyService } from './company.service';
import { Observable } from 'rxjs';
import { Company } from './company.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private _companyService: CompanyService) { }

  ngOnInit() {
    this.companies$ = this._companyService.get();
  }

  public companies$: Observable<Company[]>;
}
