import './index.css';

import React from 'react';
import ReactDOM from 'react-dom/client';
import { createBrowserRouter, Link, RouterProvider } from 'react-router-dom';

import * as auth from '@/features/auth';
import { Home } from '@/pages/Home';
import { Root, rootLoader } from '@/pages/Root';

const router = createBrowserRouter([
  {
    path: '/',
    element: <Root/>,
    // loader: rootLoader,
    errorElement:
      <div>
        Error occurred!
        <Link to="/home">Go Home</Link>
      </div>,
    children: [
      { index: true, element: <p><Link to="/home">Go Home</Link></p> },
      { path: '/home', element: <Home/> },
      {
        path: '/sign-up',
        element: <auth.SignUp/>,
        action: auth.signUpAction,
      },
      {
        path: '/sign-in',
        element: <auth.SignIn/>,
        action: auth.signInAction,
      },
    ],
  },

]);

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  <React.StrictMode>
    <RouterProvider router={router}/>
  </React.StrictMode>,
);
