import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map, retry, retryWhen } from "rxjs/operators";
import { Company } from "./company.model";

@Injectable()
export class CompanyService {
  constructor(private _client: HttpClient) { }

  public get(): Observable<Array<Company>> {
    return this._client.get<{ companies: Array<Company> }>(`/api/companies`)
      .pipe(
        map(x => x.companies)
      );
  }
}
