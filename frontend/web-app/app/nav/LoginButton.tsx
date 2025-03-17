'use client'

import { Button } from 'flowbite-react'
import { signIn } from 'next-auth/react'


export default function LoginButton() {

  return (

    <Button outline onClick={() => signIn('id-server', {callbackUrl: '/'})}>Login
     </Button>
   
  )
}


 //   const [isClient, setIsClient] = useState(false);
  // const router = useRouter();

  // useEffect(() => {
  //   setIsClient(true);
  // }, []);

  // if (!isClient) return null;
