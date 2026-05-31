import { createFileRoute } from '@tanstack/react-router'
import AddressFields from '#/components/AddressFields/AddressFields'

export const Route = createFileRoute('/')({ component: Home })

function Home() {
  return (
    <div>
      <p className="mt-4 text-lg">
        <AddressFields/>
      </p>
    </div>
  )
}
