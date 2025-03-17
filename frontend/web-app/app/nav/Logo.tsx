'use client'

import { useParamsStore } from '@/hooks/useParamsStore'
import { FaCarAlt } from 'react-icons/fa'
import { usePathname, useRouter } from "next/navigation";

export default function Logo() {
    const reset = useParamsStore(state => state.reset)
      const router = useRouter();
      const pathname = usePathname();

      function doReset(){
        if(pathname !== '/') router.push('/')

          reset();
      }

    
      
  return (
     <div onClick={doReset}  className='cursor-pointer flex items-center gap-2 text-3xl font-semibold text-red-500'>
            <FaCarAlt size={34}/>
                <div>Carluxmart Auctions</div>
            </div>
  )
}
