import { createFileRoute } from '@tanstack/react-router'
import { facilityTypeFilter, findFacilityInfo } from '../components/AddressFields/AddressFields.tsx'
import type { Facility } from '#/components/AddressFields/types.ts';

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
  const data = Route.useLoaderData() as Facility;
  const { Code, Name, Address, FacilityTypeId, BarangayId, CreatedAt, PostalCode, ContactNumber, Email, FacilityStatus  } = data.facility;

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
        </div>
}
