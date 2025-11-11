class ChangeBusinessIdNullOnEmployees < ActiveRecord::Migration[8.1]
  def change
    change_column_null :employees, :business_id, true
  end
end
