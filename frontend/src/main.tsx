import './index.css';

import { ChakraProvider } from '@chakra-ui/react';
import React from 'react';
import ReactDOM from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';

import { Link } from '@/components/link/Link';
import * as auth from '@/features/auth';
import { Home } from '@/pages/Home';
import { Root } from '@/pages/Root';

const router = createBrowserRouter([
  {
    path: '/',
    element: <Root />,
    errorElement: (
      <div>
        Error occurred!
        <Link to="/home">Go Home</Link>
      </div>
    ),
    children: [
      {
        index: true,
        element: (
          <p>
            <Link to="/home">Go Home</Link>
          </p>
        ),
      },
      { path: '/home', element: <Home /> },
      {
        path: '/sign-up',
        element: <auth.SignUp />,
        action: auth.signUpAction,
      },
      {
        path: '/sign-in',
        element: <auth.SignIn />,
        action: auth.signInAction,
      },
    ],
  },
]);

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  <React.StrictMode>
    <ChakraProvider>
      <RouterProvider router={router} />
    </ChakraProvider>
  </React.StrictMode>,
);
