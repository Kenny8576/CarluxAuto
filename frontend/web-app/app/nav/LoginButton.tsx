'use client'

import { useEffect, useState } from "react";
import { Button } from 'flowbite-react'
// import { signIn } from '@/auth'
// import Link from 'next/link'
import { useRouter } from "next/navigation";

export default function LoginButton() {

    const [isClient, setIsClient] = useState(false);
  const router = useRouter();

  useEffect(() => {
    setIsClient(true);
  }, []);

  if (!isClient) return null;

  return (

    <Button outline onClick={() => router.push('/login')}>Login
        {/* signIn('credentials', {calbackUrl: '/'}, {prompt: 'login'} */}
    </Button>
   
  )
}
