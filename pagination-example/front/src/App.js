import React from 'react';
import {ServerTable} from '@tourmalinecore/react-table-responsive';

function App() {
  return (
    <div className="App">
      <ServerTable
        tableId="uniq-table-id"
        columns={[
          {
            Header: 'Название',
            accessor: 'name',
          },
          {
            Header: 'Стоимость',
            accessor: 'cost',
            disableFilters: true,
            Cell: ({row}) => `${row.original.cost} р.`,
          },
          {
            Header: 'Производитель',
            accessor: 'vendorName',
          },
          {
            Header: 'Годен до',
            accessor: 'expirationDate',
            disableFilters: true,
            Cell: ({row}) => new Date(row.original.expirationDate).toDateString(),
          }
        ]}
        actions={[
          {
            name: 'show-action',
            show: (row) => true,
            renderIcon: () => <span>!</span>,
            renderText: (row) => `Показать сообщение`,
            onClick: (e, row) => alert(`Это сообщение о товаре "${row.original.name}"`),
          }
        ]}
        order={{
          id: 'name',
          desc: false,
        }}
        language="ru"
        apiHostUrl="http://localhost:5000"
        dataPath="/products/all"
        requestMethod="GET"
      />
    </div>
  );
}

export default App;
