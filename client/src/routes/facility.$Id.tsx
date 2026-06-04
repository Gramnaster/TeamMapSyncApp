import { createFileRoute, useHydrated } from '@tanstack/react-router'
import { facilityTypeFilter, findFacilityInfo } from '../components/AddressFields/AddressFields.tsx'
import type { Facility, Employee } from '#/components/AddressFields/types.ts';
import facilityUsers from "../../public/assets/data/facility-users.json"
import users from "../../public/assets/data/users.json"
import { useEffect, useState } from 'react';

export const Route = createFileRoute('/facility/$Id')({
  component: RouteComponent,
  loader: async ({params}) => {

    try {
      const {Id} = params
      const facility = await findFacilityInfo(Id)
      return { facility };
    } catch (error) {
      console.error(`Loader error:`, error)
      throw error
    }
  }
})


function RouteComponent() {
  const loaderData = Route.useLoaderData();
  const [ employees, setEmployees ] = useState<Employee[]>([])
  
  const { Id, Code, Name, Address, FacilityTypeId, BarangayId, CreatedAt, PostalCode, ContactNumber, Email, FacilityStatus  } = loaderData.facility as Facility;

  const findUsers = () => {
    const faciUserArr = []
    const filteredFacilitiesUsers =  facilityUsers.filter((facilityUser) => facilityUser.FacilityId === Id)
    
    for (let i = 0; i < filteredFacilitiesUsers.length; i++) {
        const faciUser = users.find((user) => user.Id === filteredFacilitiesUsers[i].UserId) as Employee;
        faciUserArr.push(faciUser)
      // Reviewing this code later
      // for (let j = 0; j < users.length; j++ ) {
      //   //  if ( filteredFacilitiesUsers[i].UserId === users[j].Id) {
      //   //   console.log(`filteredFacilitiesUsers[i] l42 : `, filteredFacilitiesUsers[i]);
      //   //   console.log(`users[j] l42 : `, users[j]);
      //   //   setEmployees([...employees, users[j]  ])
          
      //   //   console.log(`employees l43 : `, employees);
      //   //  }

      //   if (filteredFacilitiesUsers[i].UserId === users[j].Id) {
      //     console.log(`users[j] l52`, users[j]);
      //     const faciUser = users[j] 
      //   }
      // }
    }
    setEmployees(faciUserArr)
  }

  useEffect(() => {
    findUsers()
  }, [])

  return <div>
          <div className='flex flex-col m-2 mt-5 capitalize'>
            <span><strong>Code:</strong> {Code}</span> 
            <span><strong>Name:</strong> {Name}</span>
            <span ><strong>Facility Type:</strong> {facilityTypeFilter(FacilityTypeId)}</span>
            <span><strong>Status:</strong> {FacilityStatus}</span>
            <span><strong>Address:</strong> {Address}</span>
            <span><strong>Barangay ID:</strong> {BarangayId}</span>
            <span><strong>Created at:</strong> {CreatedAt.substring(0, 10)}</span>
            <span><strong>PostalCode:</strong> {PostalCode ? PostalCode : 'N/A'}</span>
            <span><strong>ContactNumber:</strong> {ContactNumber ? ContactNumber : 'N/A'}</span>
            <span><strong>Email:</strong> {Email ? Email : 'N/A'}</span>
          </div>
          <div>
            <div className='ml-3 mt-3 text-[20px] font-bold'>Employees: </div>
            {employees.length > 0 ? employees.map((employee) => {
              const { Id, Code, FirstName, MiddleName, LastName, Email, DateOfBirth, HomeAddress, CreatedAt } = employee
              return (
                <div key={Id} className='m-2'>
                  <div>
                    <div><strong>Code: </strong>{Code}</div>
                    <div><strong>FirstName: </strong>{FirstName}</div>
                    <div><strong>MiddleName: </strong>{MiddleName}</div>
                    <div><strong>LastName: </strong>{LastName}</div>
                    <div><strong>Email: </strong>{Email}</div>
                    <div><strong>DateOfBirth: </strong>{DateOfBirth}</div>
                    <div><strong>HomeAddress: </strong>{HomeAddress}</div>
                    <div><strong>CreatedAt: </strong>{CreatedAt}</div></div>
                </div>
              )
            }) : 'no employees recorded'}
          </div>
        </div>
}
