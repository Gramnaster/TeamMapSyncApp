import regions from '../../../public/assets/data/regions.json'
import provinces from '../../../public/assets/data/provinces.json'
import localGovernmentUnits from '../../../public/assets/data/local-government-units.json'
import barangays from '../../../public/assets/data/barangays.json'
import facilities from '../../../public/assets/data/facilities.json'
import { useState } from 'react'
import { Link } from '@tanstack/react-router'
import type { Province, LocalGovernmentUnit, Barangay, Facility } from './types'

export const findFacilityInfo = (id: string | number) => {
  const what = facilities.find(faci => faci.Id === id )
  return what
}

export const facilityTypeFilter = (ft: string) => {
      console.log(`ft`, ft);
     const facilityTypeFiltered = ft.slice(14)
     return facilityTypeFiltered
  }

const AddressFields = () => {
  const [ selectedRegion, setSelectedRegion ] = useState('');
  const [ selectedProvince, setSelectedProvince ] = useState('');
  const [ selectedLgu, setSelectedLgu ] = useState('');
  const [ selectedBrgy, setSelectedBrgy ] = useState('');
  const [ province, setProvince ] = useState<Province[]>([]);
  const [ lgu, setLgu ] = useState<LocalGovernmentUnit[]>([]);
  const [ brgy, setBrgy ] = useState<Barangay[]>([]);
  const [ facility, setFacility ] = useState<Facility[]>([]);

  const changeRegionHandler = (event: any) => {
    setSelectedRegion(event.target.value);
    setProvince(provinces.filter(prvnc => event.target.value === prvnc.RegionId));
  }
  
  const changeProvinceHandler = (event: any) => {
    setSelectedProvince(event.target.value);
    setLgu(localGovernmentUnits.filter(localgu => event.target.value === localgu.ProvinceId));
  }

  const changeLguHandler = (event: any) => {
    setSelectedLgu(event.target.value);
    setBrgy(barangays.filter(brg => event.target.value === brg.LocalGovernmentUnitId));
  }

  const changeBrgyHandler = (event: any) => {
    setSelectedBrgy(event.target.value);
    setFacility(facilities.filter(faci => event.target.value === faci.BarangayId));
  }

  return (
    <div>
        <div className=''>
          <div>
          <label className='font-bold'>Region:</label>
          <select value={selectedRegion} onChange={changeRegionHandler}>
            <option>--Select Region here--</option>
            {regions.map((rgn) => {
              const { Id, Name } = rgn;
              return (
                <option key={Id} value={Id}>{Name}</option>
              )
            })}
          </select>
          </div>
          
          <div className='flex'>
            <label className='font-bold '>Provinces:</label>
            <select className={ province.length <= 0 ? `block text-[#808080a9]` : `hidden` } disabled>
              <option className='text-[red]'>--Select Province here--</option>
            </select>
            <div className={ province.length <= 0 ? `hidden` : `block` }>
              <select value={selectedProvince} onChange={changeProvinceHandler}>
                <option>--Select Province here--</option>
                {province.map((prvc: Province) => {
                  const { Id, Name } = prvc;
                  return (
                    <option key={Id} value={Id}>{Name}</option>
                  )
                })}
              </select>
            </div>
          </div>
          
          <div className='flex'>
            <label className='font-bold'>LGUs:</label>
            <select className={ lgu.length <= 0 ? `block text-[#808080a9]` : `hidden` } disabled>
              <option>--Select LGU here--</option>
            </select>
            <div className={ lgu.length <= 0 ? `hidden` : `block` }>
              <select value={selectedLgu} onChange={changeLguHandler}>
                <option>--Select LGU Here--</option>
                {lgu.map((localGovU: LocalGovernmentUnit) => {
                  const { Id, Name } = localGovU;
                  return (
                    <option key={Id} value={Id}>{Name}</option>
                  )
                })}
              </select>
            </div>
          </div>
                    
          <div className='flex'>
            <label className='font-bold'>Barangays:</label>
            <select className={ brgy.length <= 0 ? `block text-[#808080a9]` : `hidden` } disabled>
              <option>--Select Barangay here--</option>
            </select>
            <div className={ brgy.length <= 0 ? `hidden` : `block` }>
              <select value={selectedBrgy} onChange={changeBrgyHandler}>
                <option>--Select Barangay here--</option>
                {brgy.map((barangy: Barangay) => {
                  const { Id, Name } = barangy;
                  return (
                    <option key={Id} value={Id}>{Name}</option>
                  )
                })}
              </select>
            </div>
          </div>
                    
          <div>
            {facility.map((fclty: Facility) => {
              const { Id, Code, Name, Address, FacilityTypeId, BarangayId, CreatedAt } = fclty;
              // Converted Id to pure string as Facility's Id property is set to 'string | number' instead of being a pure string, therefore TS treats it as unsafe
              const convertedId = Id.toString();
              console.log(`convertedId`, convertedId);
              return (
                <div key={Id} className='flex flex-col m-2 mt-5 capitalize'>
                  <span><strong>Code:</strong> {Code}</span>
                  <span><strong>Name:</strong> {Name}</span>
                  <span ><strong>Facilty Type:</strong> {facilityTypeFilter(FacilityTypeId)}</span>
                  <span><strong>Address:</strong> {Address}</span>
                  <span><strong>Barangay ID:</strong> {BarangayId}</span>
                  <span><strong>Created at: </strong>{CreatedAt.substring(0, 10)}</span>
                  <Link to={`/facility/$Id`} params={{Id:convertedId}} >Facility Info here:</Link>
                </div>
              )
            })}
          </div>

        </div>
    </div>
  )
}

export default AddressFields