import './index.scss';

import { ChakraProvider } from '@chakra-ui/react';
import React from 'react';
import ReactDOM from 'react-dom/client';
import {
  createBrowserRouter,
  RouteObject,
  RouterProvider,
} from 'react-router-dom';

import { signInRoute, signUpRoute } from '@/features/auth';
import {
  dashboardRoute,
  homeRoute,
  rootIndexRoute,
  rootRoute,
} from '@/routing/routes';

const router = createBrowserRouter([
  {
    ...rootRoute,
    children: [
      rootIndexRoute,
      homeRoute,
      signInRoute,
      signUpRoute,
      dashboardRoute,
    ],
  } as RouteObject,
]);

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  <React.StrictMode>
    <ChakraProvider>
      <RouterProvider router={router} />
    </ChakraProvider>
  </React.StrictMode>,
);
