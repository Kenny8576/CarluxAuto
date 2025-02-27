'use client'

import { useParamsStore } from '@/hooks/useParamsStore'
import React, { useEffect, useState } from 'react'
import { FaCarAlt } from 'react-icons/fa'
import { useRouter } from "next/navigation";

export default function Logo() {
    const reset = useParamsStore(state => state.reset)
    
        const [isClient, setIsClient] = useState(false);
      const router = useRouter();
    
      useEffect(() => {
        setIsClient(true);
      }, []);
    
      if (!isClient) return null;
      
  return (
     <div onClick={reset}  className='cursor-pointer flex items-center gap-2 text-3xl font-semibold text-red-500'>
            <FaCarAlt size={34}/>
                <div onClick={() => router.push('/')}>Carluxmart Auctions</div>
            </div>
  )
}
