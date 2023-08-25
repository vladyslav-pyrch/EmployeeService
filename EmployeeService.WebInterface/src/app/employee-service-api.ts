import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Company} from "./contracts/company";
import {environment} from "../environments/environment";
import {Department} from './contracts/department';
import {Employee} from './contracts/employee';
import {Observable} from 'rxjs';

type HttpOption = {
	observe: 'body',
	responseType: 'json'
};

@Injectable({
	providedIn: "root"
})
export class EmployeeServiceApi {
	private readonly httpOptions: HttpOption = {
		observe: 'body' as const,
		responseType: 'json' as const
	};

	public constructor(private http : HttpClient) {
	}

	public addDepartmentToCompany(body: { companyId: number, departmentName: string, departmentPhoneNumber: string }):
		Observable<{ departmentId: number }> {
		return this.http.post<{ departmentId: number }>(
			environment.apiUrl + 'api/Company/AddDepartmentToCompany',
			body,
			this.httpOptions
		);
	}

	public createCompany(body: { name: string, phoneNumber: string }): Observable<{ companyId: number }> {
		return this.http.post<{ companyId: number }>(
			environment.apiUrl + 'api/Company/CreateCompany',
			body,
			this.httpOptions
		);
	}

	public createEmployee(body: { name: string; surname: string; passportNumber: string;
		passportType: string; phoneNumber: string; companyId: number; departmentId: number;
	}): Observable<{ employeeId: number }> {
		return this.http.post<{ employeeId: number }>(
			environment.apiUrl + 'api/Employee/CreateEmployee',
			body,
			this.httpOptions
		);
	}

	public getAllCompanies(): Observable<{ companies: Company[] }> {
		return this.http.get<{ companies: Company[] }>(
			environment.apiUrl + 'api/Company/GetAllCompanies',
			this.httpOptions
		);
	}

	public getAllDepartmentsOfCompany(companyId: number): Observable<{ departments: Department[] }> {
		return this.http.get<{ departments: Department[] }>(
			environment.apiUrl + `api/Company/GetAllDepartmentsFromCompany?CompanyId=${companyId}`,
			this.httpOptions
		);
	}

	public getAllEmployeesFromCompany(companyId: number): Observable<{ employees: Employee[] }> {
		return this.http.get<{ employees: Employee[] }>(
			environment.apiUrl + `api/Employee/GetAllEmployeesFromCompany?CompanyId=${companyId}`,
			this.httpOptions
		);
	}

	public getAllEmployeesFromDepartment(departmentId: number): Observable<{ employees: Employee[] }> {
		return this.http.get<{ employees: Employee[] }>(
			environment.apiUrl + `api/Employee/GetAllEmployeesFromDepartment?DepartmentId=${departmentId}`,
			this.httpOptions
		);
	}

	public getCompanyById(companyId: number): Observable<{ company: Company }> {
		return this.http.get<{ company: Company }>(
			environment.apiUrl + `api/Company/GetCompanyById?CompanyId=${companyId}`,
			this.httpOptions
		);
	}

	public getDepartmentById(departmentId: number): Observable<{ department: Department }> {
		return this.http.get<{ department: Department }>(
			environment.apiUrl + `api/Company/GetDepartmentById?DepartmentId=${departmentId}`,
			this.httpOptions
		);
	}

	public getEmployeeById(employeeId: number): Observable<{ employee: Employee }> {
		return this.http.get<{ employee: Employee }>(
			environment.apiUrl + `api/Employee/GetEmployeeById?EmployeeId=${employeeId}`,
			this.httpOptions
		);
	}

	public updateCompany(body: { companyId: number; name?: string; }): Observable<unknown> {
		return this.http.patch<unknown>(
			environment.apiUrl + 'api/Company/UpdateCompany',
			body
		);
	}

	public updateDepartment(body: { departmentId: number; name?: string; phoneNumber?: string; }): Observable<unknown> {
		return this.http.patch<unknown>(
			environment.apiUrl + 'api/Company/UpdateDepartment',
			body
		);
	}

	public updateEmployee(body: { id: number; name: string; surname: string; passportNumber: string;
		passportType: string; phoneNumber: string; companyId: number; departmentId: number; })
		: Observable<unknown> {
		return this.http.patch<unknown>(
			environment.apiUrl + 'api/Company/UpdateCompany',
			body
		);
	}

	public deleteCompany(companyId: number): Observable<unknown> {
		return this.http.delete(
			environment.apiUrl + `api/Company/DeleteCompany?CompanyId=${companyId}`
		);
	}

	public deleteDepartment(departmentId: number): Observable<unknown> {
		return this.http.delete(
			environment.apiUrl + `api/Company/DeleteDepartment?DepartmentId=${departmentId}`
		);
	}

	public deleteEmployee(employeeId: number): Observable<unknown> {
		return this.http.delete(
			environment.apiUrl + `api/Employee/DeleteEmployee?EmployeeId=${employeeId}`
		);
	}
}
