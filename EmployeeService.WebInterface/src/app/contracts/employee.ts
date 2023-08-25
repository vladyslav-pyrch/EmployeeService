import {Department} from './department';
import {Passport} from './passport';

export class Employee {
	public id: number;

	public name: string;

	public surname: string;

	public phone: string;

	public companyId: number;

	public department: Department;

	public passport: Passport;

	public constructor(
		id: number,
		name: string,
		surname: string,
		phone: string,
		companyId: number,
		department: Department,
		passport: Passport) {
		this.id = id;
		this.name = name;
		this.surname = surname;
		this.phone = phone;
		this.companyId = companyId;
		this.department = department;
		this.passport = passport;
	}
}
