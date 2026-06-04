export interface Region {
  Id: number | string;
  Name: string;
  CountryId: string;
}

export interface Province {
  Id: number | string;
  Name: string;
  RegionId: number | string;
}

export interface LocalGovernmentUnit {
  Id: number | string;
  Name: string;
  ProvinceId: number | string;
  Type: string;
}

export interface Barangay {
  Id: number | string;
  Name: string;
  LocalGovernmentUnitId: number | string;
}

export interface Facility {
  Id: number | string;
  Code: string;
  Name: string;
  FacilityTypeId: string;
  BarangayId: string;
  Latitude: number;
  Longitude: number;
  Address: string;
  PostalCode: null;
  ContactNumber: null;
  Email: null;
  FacilityStatus: string;
  CreatedAt: string;
  UpdatedAt: string;
}

export interface Employee {
  Id: string;
  Code: string;
  FirstName: string;
  MiddleName: string;
  LastName: string;
  Email: string;
  PasswordHash: string;
  DateOfBirth: string;
  HomeAddress: string;
  // isActive: boolean;
  // isDeleted: boolean;
  DeletedAt: string | null;
  CreatedAt: string;
  UpdatedAt: string;
}