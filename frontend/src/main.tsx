import './index.scss';

import { ChakraProvider } from '@chakra-ui/react';
import React from 'react';
import ReactDOM from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';

import {
  authRoute,
  dashboardRoute,
  homeRoute,
  rootIndexRoute,
  rootRoute,
} from '@/routing/routes';

const router = createBrowserRouter([
  {
    ...rootRoute,
    children: [rootIndexRoute, authRoute, homeRoute, dashboardRoute],
  },
]);

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  <React.StrictMode>
    <ChakraProvider>
      <RouterProvider router={router} />
    </ChakraProvider>
  </React.StrictMode>,
);
